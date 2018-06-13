import { Component, OnInit, Inject } from '@angular/core';
import { BASE_PATH, AdminService, AnalyticsService, ReportsTotalResponse } from '../../swagger-gen';
import { environment } from '../../environments/environment';
import { AuthService } from '../services/auth.service';
import { Chart } from 'angular-highcharts';
import { GroupBy } from '../../utils/GroupBy';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public constructor(private analyticsService: AnalyticsService) { }

  public reportsTotalData: ReportsTotalResponse[] = null;

  public feedbackByUserChart: Chart = null;
  public reportsOverTimeChart: Chart = null;
  public reportsTotalChart: Chart = null;
  public reportsReasonsChart: Chart = null;
  public reportsFeedbackChart: Chart = null;

  ngOnInit() {
    this.updateCharts();
  }

  private updateCharts() {
    this.updateFeedbackByUserChart();
    this.updateOverTimeChart();
    this.updateTotalChart();
    this.updateReasonsChart();
    this.updateFeedbackChart();
  }

  private updateFeedbackByUserChart() {
    this.analyticsService.analyticsFeedbackByUserGet().subscribe(totalData => {
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
    this.analyticsService.analyticsReportsOverTimeGet().subscribe(overTimeData => {
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

  private updateTotalChart() {
    this.analyticsService.analyticsReportsTotalGet().subscribe(totalData => {
      this.reportsTotalData = totalData;
      const mappedData = totalData.map(points => ({ name: points.dashboardName, y: points.count }));
      this.reportsTotalChart = new Chart({
        chart: {
          type: 'pie'
        },
        title: {
          text: 'Total reports by dashboard'
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

  private updateReasonsChart() {
    this.analyticsService.analyticsReportsByReasonGet().subscribe(totalData => {
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
    this.analyticsService.analyticsReportsByFeedbackGet().subscribe(totalData => {
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

  public onRowClick(dashboardName: string) {
    window.location.href = `/${dashboardName}/`;
  }
}
