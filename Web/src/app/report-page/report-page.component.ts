import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewerService, ReviewerReportResponse } from '../../swagger-gen';
import { MetaDataService } from '../services/meta-data.service';

@Component({
  selector: 'app-report-page',
  templateUrl: './report-page.component.html',
  styleUrls: ['./report-page.component.scss']
})
export class ReportPageComponent implements OnInit {
  public report: ReviewerReportResponse;
  public reportNotFound: boolean;

  private currentReportId?: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private reviewerService: ReviewerService,
    private metaDataService: MetaDataService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const dashboardName = params['dashboardName'];
      const reportId = +params['id'];

      if (reportId !== this.currentReportId) {
        this.currentReportId = reportId;
        this.reviewerService.reviewerReportGet(reportId).subscribe(response => {
          if (!response) {
            this.reportNotFound = true;
          } else {
            this.reportNotFound = false;
            this.report = response;
            if (response.dashboardName && dashboardName !== response.dashboardName) {
              // Wait a short time so people can hit 'back' in their browsers quickly
              const previousLocation = window.location.toString();
              setTimeout(() => {
                if (window.location.toString() === previousLocation) {
                  // Don't redirect if they've changed the page already
                  this.router.navigateByUrl(`/${response.dashboardName}/report/${reportId}`);
                }
              }, 500);
            }

            if (this.report.tabTitle && this.report.tabTitle !== '') {
              this.metaDataService.setTitle(`${this.report.tabTitle} - ${this.report.title}`);
            }
            if (this.report.favIcon && this.report.favIcon !== '') {
              this.metaDataService.setFavIcon(this.report.favIcon);
            }
          }
        });
      }
    });
  }

  public OnFeedbackClicked() {
    window.location.reload();
  }
  public OnFeedbackCleared() {
    window.location.reload();
  }
}
