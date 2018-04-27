import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestOptions, Headers } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import { HttpRequestInterceptorService, HttpInterception } from '../http-request-interceptor.service';
import { ProfileResult } from '../models/ProfileResult';

const MiniProfilerIDsHeader = 'X-MiniProfiler-Ids';
const MiniProfilerEndPoint = 'mini-profiler-resources/results';
@Component({
    selector: 'app-mini-profiler-ui',
    templateUrl: './mini-profiler-ui.component.html',
    styleUrls: ['./mini-profiler-ui.component.scss']
})
export class MiniProfilerUiComponent implements OnInit {
    private originalSend: (data?: Document) => void;
    private fetchedIds: string[] = [];
    public results: ProfileResult[] = [];
    private selectedResult;

    public pendingCount = 0;

    @Input()
    public Hidden = false;
    @Input()
    public TrivialMilliseconds = 50;
    @Input()
    public ProfilerWidth = 85;
    @Input()
    public Position: 'Left' | 'Right' = 'Left';
    @Input()
    public MaxEntries = 15;
    @Input()
    public ValidUrl: (interception: HttpInterception) => boolean = (interception: HttpInterception) => true

    constructor(private httpClient: HttpClient, private httpRequestInterceptor: HttpRequestInterceptorService) {
    }

    private childClicked(event) {
        if (this.selectedResult === event) {
            this.selectedResult = null;
        } else {
            this.selectedResult = event;
        }
    }

    public toggleGridVisible() {
        this.Hidden = !this.Hidden;
    }

    public clearGrid() {
        this.results = [];
    }

    public onClickedOutside(event) {
        this.selectedResult = null;
    }

    public identify (index, item) {
        return item.Id;
    }

    private GetResults(ids: string[], miniProfilerUrl: string) {
        for (let i = 0; i < ids.length; i++) {
            const id = ids[i];
            if (this.fetchedIds.indexOf(id) >= 0) {
                return;
            }
            this.fetchedIds.push(id);

            const body = new FormData();
            body.append('id', id);
            body.append('popup', '1');

            this.httpClient.post(miniProfilerUrl + MiniProfilerEndPoint, body).subscribe((result: ProfileResult) => {
                this.processJson(result);
                while (this.MaxEntries > 0 && this.results.length >= this.MaxEntries) {
                    this.results.shift();
                }
                this.results.push(result);
            });
        }
    }

    // Takes a string like http://localhost:8081/api/graphql and returns http://localhost:8081
    private GetOrigin(url: string) {
        const tmp = document.createElement('a') as any; // Typescript not recognizing .origin;
        tmp.href = url;
        return `${tmp.origin}/`;
    }

    // This logic is taken from the MiniProfilerUI
    // It's likely this can be significantly simplified...
    private processJson(json) {
        json.HasDuplicateCustomTimings = false;
        json.HasCustomTimings = false;
        json.HasTrivialTimings = false;
        json.CustomTimingStats = {};
        json.CustomLinks = json.CustomLinks || {};
        json.TrivialMilliseconds = this.TrivialMilliseconds;
        json.Root.ParentTimingId = json.Id;

        // different serializers handle dates differently
        switch (typeof json.Started) {
            case 'number':
                json.Started = new Date(json.Started);
                break;
            case 'string':
                // .NET's JavaScriptSerializer sends dates as /Date(1308024322065)/
                const array = /-?\d+/.exec(json.Started);
                if (array.length === 1) {
                    json.Started = new Date(parseInt(array[0], 10));
                }
                break;
        }

        this.processTiming(json, json.Root, 0);
    }

    private processTiming(json, timing, depth) {
        timing.DurationWithoutChildrenMilliseconds = timing.DurationMilliseconds;
        timing.Depth = depth;
        timing.HasCustomTimings = timing.CustomTimings ? true : false;
        timing.HasDuplicateCustomTimings = {};
        json.HasCustomTimings = json.HasCustomTimings || timing.HasCustomTimings;

        if (timing.Children) {
            for (let i = 0; i < timing.Children.length; i++) {
                timing.Children[i].ParentTimingId = timing.Id;
                this.processTiming(json, timing.Children[i], depth + 1);
                timing.DurationWithoutChildrenMilliseconds -= timing.Children[i].DurationMilliseconds;
            }
        } else {
            timing.Children = [];
        }

        // do this after subtracting child durations
        timing.IsTrivial = timing.DurationMilliseconds < this.TrivialMilliseconds;
        json.HasTrivialTimings = json.HasTrivialTimings || timing.IsTrivial;

        if (timing.CustomTimings) {
            timing.CustomTimingStats = {};
            for (const customType in timing.CustomTimings) {
                if (!timing.CustomTimings.hasOwnProperty(customType)) {
                    continue;
                }
                const customTimings = timing.CustomTimings[customType];
                const customStat = {
                    Duration: 0,
                    Count: 0
                };
                const duplicates = {};
                for (let i = 0; i < customTimings.length; i++) {
                    const customTiming = customTimings[i];
                    customTiming.ParentTimingId = timing.Id;
                    customStat.Duration += customTiming.DurationMilliseconds;
                    customStat.Count++;
                    if (customTiming.CommandString && duplicates[customTiming.CommandString]) {
                        customTiming.IsDuplicate = true;
                        timing.HasDuplicateCustomTimings[customType] = true;
                        json.HasDuplicateCustomTimings = true;
                    } else {
                        duplicates[customTiming.CommandString] = true;
                    }
                }
                timing.CustomTimingStats[customType] = customStat;
                if (!json.CustomTimingStats[customType]) {
                    json.CustomTimingStats[customType] = {
                        Duration: 0,
                        Count: 0
                    };
                }
                json.CustomTimingStats[customType].Duration += customStat.Duration;
                json.CustomTimingStats[customType].Count += customStat.Count;
            }
        } else {
            timing.CustomTimings = {};
        }
    }

    ngOnInit() {
        const seenRequests = [];
        this.httpRequestInterceptor.ReadyStateChangedObservable().subscribe(event => {
            if (event.request.readyState === 4) {
                const requestIndex = seenRequests.indexOf(event.request);
                if (requestIndex > -1) {
                    seenRequests.splice(requestIndex, 1);
                    this.pendingCount--;
                }
            }

            const request: XMLHttpRequest = event.request;
            if (!this.ValidUrl(event)) {
                return;
            }
            const ids = JSON.parse(request.getResponseHeader(MiniProfilerIDsHeader)) as string[];
            if (ids) {
                const miniProfilerUrl = this.GetOrigin(event.request.responseURL);
                this.GetResults(ids, miniProfilerUrl);
            }
        });
        this.httpRequestInterceptor.SentObservable().subscribe((request) => {
            seenRequests.push(request);
            this.pendingCount++;
        });
    }
}
