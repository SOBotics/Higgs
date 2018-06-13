import { Component, OnInit } from '@angular/core';
import { ReviewerService, AnalyticsService, PagingResponseReviewerReportsResponse } from '../../swagger-gen';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupBy } from '../../utils/GroupBy';
import { Chart } from 'angular-highcharts';
import { GetPagingInfo } from '../../utils/PagingHelper';
import { IMultiSelectTexts, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import Highcharts = require('highcharts');

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public botId: number;
  public initialized: boolean;
  public validDashboard: boolean;
  public dashboardName: string;

  public dropdownSettings: IMultiSelectSettings;
  public feedbackSelector: IMultiSelectTexts;
  public reasonSelector: IMultiSelectTexts;

  public feedbackByUserChart: Chart = null;
  public reportsOverTimeChart: Chart = null;
  public reportsReasonsChart: Chart = null;
  public reportsFeedbackChart: Chart = null;

  public feedbacks: any[];
  public reasons: any[];

  public reportsResponse: PagingResponseReviewerReportsResponse = null;

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
    private router: Router) {

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
    this.route.params.subscribe(params => {
      this.dashboardName = params['dashboardName'];
      this.refreshData();
    });
  }

  private refreshData() {
    this.initialized = false;
    this.validDashboard = false;
    this.reviewerService.reviewerBotByDashboardGet(this.dashboardName).subscribe(botId => {
      this.botId = botId;
      this.validDashboard = botId > 0;
      this.initialized = true;

      this.reviewerService.reviewerFeedbacksGet(this.dashboardName)
        .subscribe(feedbacks => {
          this.feedbacks = feedbacks.map(feedback => ({ id: feedback.id, name: feedback.name }));
        });

      this.reviewerService.reviewerReasonsGet(this.dashboardName)
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
    this.analyticsService.analyticsFeedbackByUserGet(this.dashboardName).subscribe(totalData => {
      const mappedData = totalData.map(points => ({ name: points.name, y: points.count }));
      this.feedbackByUserChart = new Chart({
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
      });
    });
  }


  private updateOverTimeChart() {
    this.analyticsService.analyticsReportsOverTimeGet(this.dashboardName).subscribe(overTimeData => {
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
      this.reportsOverTimeChart = new Chart({
        chart: {
          type: 'line',
        },
        title: {
          text: 'Reports per dashboard over time'
        },
        xAxis: {
          type: 'datetime',
          labels: {
            format: '{value:%Y-%m-%d}'
          }
        },
        tooltip: {
          formatter: function () {
            const date = new Date(this.x);
            return `<b>${this.series.name}</b><br/>${Highcharts.dateFormat('%Y-%m-%d', this.x)} - ${this.y} seen`;
          }
        },
        yAxis: {
          title: {
            text: 'Number seen'
          }
        },
        credits: {
          enabled: false
        },
        series: series
      });
    });
  }

  private updateReasonsChart() {
    this.analyticsService.analyticsReportsByReasonGet(this.dashboardName).subscribe(totalData => {
      const mappedData = totalData.map(points => ({ name: points.name, y: points.count }));
      this.reportsReasonsChart = new Chart({
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
      });
    });
  }

  private updateFeedbackChart() {
    this.analyticsService.analyticsReportsByFeedbackGet(this.dashboardName).subscribe(totalData => {
      const mappedData = totalData.map(points => ({ name: points.name, y: points.count }));
      this.reportsFeedbackChart = new Chart({
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
      });
    });
  }


  public getPages() {
    return GetPagingInfo(this.reportsResponse);
  }

  public loadPage(pageNumber: number) {
    this.filter.pageNumber = pageNumber;
    this.applyFilter();
  }

  public applyFilter() {
    this.router.navigate(['/reports'], { queryParams: this.filter });
  }

  public reloadData() {
    const conflicted =
      this.filter.conflicted === 'any' ? null
        : this.filter.conflicted === 'yes' ? true : false;

    const hasFeedback =
      this.filter.hasFeedback === 'any' ? null
        : this.filter.hasFeedback === 'yes' ? true : false;

    // const botId = this.filter.dashboard < 0 ? null : this.filter.dashboard;

    this.reviewerService.reviewerReportsGet(
      this.filter.content,
      this.botId,
      hasFeedback,
      conflicted,
      this.filter.feedbacks,
      this.filter.reasons,
      this.filter.pageNumber,
      50
    ).subscribe(response => {
      if (response.pageNumber > response.totalPages) {
        this.filter.pageNumber = 1;
        this.reloadData();
      } else {
        this.reportsResponse = response;
      }
    });
  }
}
