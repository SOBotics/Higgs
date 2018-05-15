import { Component, OnInit, Inject } from '@angular/core';
import { BASE_PATH, AdminService, AnalyticsService } from '../../swagger-gen';
import { environment } from '../../environments/environment';
import { AuthService } from '../services/auth.service';
import { Chart } from 'angular-highcharts';
import { GroupBy } from '../../utils/GroupBy';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public constructor(private analyticsService: AnalyticsService) { }

  public reportsOverTimeChart: Chart = null;
  public reportsTotalChart: Chart = null;
  public reportsReasonsChart: Chart = null;
  public reportsFeedbackChart: Chart = null;

  ngOnInit() {
    this.updateCharts();
  }

  private updateCharts() {
    this.updateOverTimeChart();
    this.updateTotalChart();
    this.updateReasonsChart();
    this.updateFeedbackChart();
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
            const utcDate = Date.UTC(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(),
              date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
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
      const mappedData = totalData.map(points => ({ name: points.dashboardName, y: points.count }));
      this.reportsTotalChart = new Chart({
        chart: {
          type: 'pie'
        },
        title: {
          text: 'Total reports by dashboard'
        },
        series: [{
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
          data: mappedData
        }],
        credits: {
          enabled: false
        },
      });
    });
  }
}
