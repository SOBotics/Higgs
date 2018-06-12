import { Component, OnInit } from '@angular/core';
import { ReviewerService, PagingResponseReviewerReportsResponse } from '../../swagger-gen';
import { ActivatedRoute, Router } from '@angular/router';
import { GetPagingInfo } from '../../utils/PagingHelper';
import { IMultiSelectTexts, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {
  public dropdownSettings: IMultiSelectSettings;
  public feedbackSelector: IMultiSelectTexts;
  public reasonSelector: IMultiSelectTexts;

  public reportsResponse: PagingResponseReviewerReportsResponse = null;

  public feedbacks: any[];
  public reasons: any[];
  public dashboards: any[];

  public filter = {
    pageNumber: 1,
    content: '' as string,
    dashboard: -1 as number,
    conflicted: 'any' as 'any' | 'yes' | 'no',
    feedbacks: [] as number[],
    reasons: [] as number[]
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private reviewerService: ReviewerService) {

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
      const pageNumber = +params['pageNumber'] || 1;
      this.filter.pageNumber = pageNumber;
      this.reloadData();
    });

    this.reviewerService.reviewerDashboardsGet()
      .subscribe(dashboards => {
        this.dashboards = dashboards.map(dashboard => ({ id: dashboard.id, name: dashboard.name }));
      });

    this.reviewerService.reviewerFeedbacksGet()
      .subscribe(feedbacks => {
        this.feedbacks = feedbacks.map(feedback => ({ id: feedback.id, name: feedback.name }));
      });

    this.reviewerService.reviewerReasonsGet()
      .subscribe(reasons => {
        this.reasons = reasons.map(reason => ({ id: reason.id, name: reason.name }));
      });
  }

  public getPages() {
    return GetPagingInfo(this.reportsResponse);
  }

  public loadPage(pageNumber: number) {
    this.router.navigateByUrl(`/reports?pageNumber=${pageNumber}`);
  }

  public reloadData() {
    const conflicted =
      this.filter.conflicted === 'any' ? null
        : this.filter.conflicted === 'yes' ? true : false;
    const botId = this.filter.dashboard < 0 ? null : this.filter.dashboard;

    this.reviewerService.reviewerReportsGet(
      this.filter.content,
      botId,
      conflicted,
      this.filter.feedbacks,
      this.filter.reasons,
      this.filter.pageNumber,
      50
    ).subscribe(response => this.reportsResponse = response);
  }
}
