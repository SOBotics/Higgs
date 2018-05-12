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

  ngOnInit() {
    this.analyticsService.analyticsReportsOverTimeGet().subscribe(overTimeData => {
      const groupedData = GroupBy(overTimeData, 'dashboardName');
      const series: { name: string, data: [number, number][] }[] = [];
      for (const key in groupedData) {
        if (groupedData.hasOwnProperty(key)) {
          series.push({
            name: key,
            data: groupedData[key].map(gd => {
              const date = new Date(gd.date);
              // Why..?
              const utcDate = Date.UTC(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(),
                date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
              return [utcDate, gd.count];
            })
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
          },
          tickInterval: 1
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
      console.log(series);
    });
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
}
