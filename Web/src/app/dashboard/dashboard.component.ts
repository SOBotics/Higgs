import { Component, OnInit } from '@angular/core';
import { ReviewerService, AnalyticsService, PagingResponseReviewerReportsResponse, ReviewerDashboardResponse } from '../../swagger-gen';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupBy } from '../../utils/GroupBy';
import { Chart } from 'angular-highcharts';
import { GetPagingInfo, PagingInfo } from '../../utils/PagingHelper';
import { IMultiSelectTexts, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import * as Highcharts from 'highcharts';
import { MetaDataService } from '../services/meta-data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public initialized: boolean;
  public validDashboard: boolean;

  public dashboard: ReviewerDashboardResponse;

  public dropdownSettings: IMultiSelectSettings;
  public feedbackSelector: IMultiSelectTexts;
  public reasonSelector: IMultiSelectTexts;

  public feedbackByUserChart: Chart = null;
  public reportsOverTimeChart: Chart = null;
  public reportsReasonsChart: Chart = null;
  public reportsFeedbackChart: Chart = null;

  public feedbacks: { id: number; name: string }[];
  public reasons: { id: number; name: string }[];

  public reportsResponse: PagingResponseReviewerReportsResponse = null;
  public pagingInfo: PagingInfo[];

  public filter = {
    pageNumber: 1,
    content: '' as string,
    hasFeedback: 'any' as 'any' | 'yes' | 'no',
    conflicted: 'any' as 'any' | 'yes' | 'no',
    feedbacks: [] as number[],
    reasons: [] as number[]
  };

  constructor(
    private reviewerService: ReviewerService,
    private analyticsService: AnalyticsService,
    private route: ActivatedRoute,
    private router: Router,
    private metaDataService: MetaDataService) {

    this.dropdownSettings = {
      checkedStyle: 'fontawesome',
      buttonClasses: 'btn btn-default',
      dynamicTitleMaxItems: 10
    };
    this.feedbackSelector = {
      defaultTitle: 'Select feedback'
    };
    this.reasonSelector = {
      defaultTitle: 'Select reasons'
    };
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.filter.pageNumber = +params['pageNumber'] || 1;
      this.filter.content = params['content'];
      this.filter.hasFeedback = params['hasFeedback'] || 'any';
      this.filter.conflicted = params['conflicted'] || 'any';

      const feedbacks = params['feedbacks'];
      this.filter.feedbacks = !feedbacks ? [] : typeof feedbacks === 'string' ? [parseInt(feedbacks, 10)] : feedbacks.map(f => parseInt(f, 10));

      const reasons = params['reasons'];
      this.filter.reasons = !reasons ? [] : typeof reasons === 'string' ? [parseInt(reasons, 10)] : reasons.map(f => parseInt(f, 10));

      this.reloadData();
    });

    this.route.params.subscribe(params => {
      const dashboardName = params['dashboardName'];
      this.refreshData(dashboardName);
    });
  }

  private refreshData(dashboardName: string) {
    this.initialized = false;
    this.validDashboard = false;
    this.reviewerService.reviewerDashboardGet(dashboardName).subscribe(response => {
      this.validDashboard = !!response;
      if (this.validDashboard) {
        this.dashboard = response;

        if (this.dashboard.tabTitle && this.dashboard.tabTitle !== '') {
          this.metaDataService.setTitle(this.dashboard.tabTitle);
        }
        if (this.dashboard.favIcon && this.dashboard.favIcon !== '') {
          this.metaDataService.setFavIcon(this.dashboard.favIcon);
        }
      }
      this.initialized = true;

      this.reviewerService.reviewerFeedbacksGet(dashboardName)
        .subscribe(feedbacks => {
          this.feedbacks = feedbacks.map(feedback => ({ id: feedback.id, name: feedback.name }));
        });

      this.reviewerService.reviewerReasonsGet(dashboardName)
        .subscribe(reasons => {
          this.reasons = reasons.map(reason => ({ id: reason.id, name: reason.name }));
        });

      this.reloadData();

      this.updateCharts();
    });
  }

  private updateCharts() {
    this.updateFeedbackByUserChart();
    this.updateOverTimeChart();
    this.updateReasonsChart();
    this.updateFeedbackChart();
  }

  private updateFeedbackByUserChart() {
    this.analyticsService.analyticsFeedbackByUserGet(this.dashboard.dashboardName).subscribe(totalData => {
      const mappedData = totalData.map(points => ({ name: points.name, y: points.count }));
      this.feedbackByUserChart =
        mappedData.length ?
          new Chart({
            chart: {
              type: 'pie'
            },
            title: {
              text: 'Top users by feedback'
            },
            series: [{
              name: 'Count',
              data: mappedData
            }],
            credits: {
              enabled: false
            },
          })
          : null;
    });
  }


  private updateOverTimeChart() {
    this.analyticsService.analyticsReportsOverTimeGet(this.dashboard.dashboardName).subscribe(overTimeData => {
      const groupedData = GroupBy(overTimeData, 'dashboardName');
      const series: { name: string, data: [number, number][] }[] = [];
      for (const key in groupedData) {
        if (groupedData.hasOwnProperty(key)) {
          const data = groupedData[key].map(gd => {
            const date = new Date(gd.date);
            // Why..?
            const utcDate = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds());
            return [utcDate, gd.count];
          });
          data.sort((left, right) => left[0] - right[0]);
          series.push({
            name: key,
            data: data
          });
        }
      }
      this.reportsOverTimeChart =
        series.length ?
          new Chart({
            chart: {
              type: 'line',
            },
            title: {
              text: 'Reports over time'
            },
            xAxis: {
              type: 'datetime',
              labels: {
                format: '{value:%Y-%m-%d}',
                rotation: -45,
              }
            },
            tooltip: {
              formatter: function () {
                return `${this.y} reports (${Highcharts.dateFormat('%Y-%m-%d', this.x)})`;
              }
            },
            yAxis: {
              title: {
                text: 'Number seen'
              }
            },
            legend: {
              enabled: false
            },
            credits: {
              enabled: false
            },
            series: series
          })
          : null;
    });
  }

  private updateReasonsChart() {
    this.analyticsService.analyticsReportsByReasonGet(this.dashboard.dashboardName).subscribe(totalData => {
      const mappedData = totalData.map(points => ({ name: points.name, y: points.count }));
      this.reportsReasonsChart =
        mappedData.length
          ?
          new Chart({
            chart: {
              type: 'pie'
            },
            title: {
              text: 'Top 15 report reasons (tripped)'
            },
            series: [{
              name: 'Count',
              data: mappedData
            }],
            credits: {
              enabled: false
            },
          })
          : null;
    });
  }

  private updateFeedbackChart() {
    this.analyticsService.analyticsReportsByFeedbackGet(this.dashboard.dashboardName).subscribe(totalData => {
      const mappedData = totalData.map(points => ({ name: points.name, y: points.count }));
      this.reportsFeedbackChart =
        mappedData.length
          ?
          new Chart({
            chart: {
              type: 'pie'
            },
            title: {
              text: 'Top 15 report feedback'
            },
            series: [{
              name: 'Count',
              data: mappedData
            }],
            credits: {
              enabled: false
            },
          }) 
          : null;
    });
  }

  public loadPage(pageNumber: number) {
    this.filter.pageNumber = pageNumber;
    this.applyFilter();
  }

  public applyFilter() {
    this.router.navigate(['/' + this.dashboard.dashboardName], { queryParams: this.filter });
  }

  public reloadData() {
    if (!this.dashboard) {
      return;
    }

    const conflicted =
      this.filter.conflicted === 'any' ? null
        : this.filter.conflicted === 'yes' ? true : false;

    const hasFeedback =
      this.filter.hasFeedback === 'any' ? null
        : this.filter.hasFeedback === 'yes' ? true : false;

    this.reviewerService.reviewerReportsGet(
      this.filter.content,
      this.dashboard.dashboardId,
      hasFeedback,
      conflicted,
      this.filter.feedbacks,
      this.filter.reasons,
      this.filter.pageNumber,
      50
    ).subscribe(response => {
      if (response.pageNumber > response.totalPages && response.totalPages > 0) {
        this.filter.pageNumber = 1;
        this.reloadData();
      } else {
        this.reportsResponse = response;
        this.pagingInfo = GetPagingInfo(response);
      }
    });
  }
}
