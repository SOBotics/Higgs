import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TimeAgoPipe } from '../pipes/TimeAgoPipe';
import { ReviewerService, ReviewerReportResponse } from '../../swagger-gen';
import { AuthService } from '../services/auth.service';
import { MetaDataService } from '../services/meta-data.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  public postDetails: ReviewerReportResponse;
  public isLoggedIn: boolean;
  public reportNotFound: boolean;

  private currentReportId?: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private reviewerService: ReviewerService,
    private metaDataService: MetaDataService
  ) { }

  public feedbackClicked(id: number) {
    this.reviewerService.reviewerSendFeedbackPost(this.currentReportId, id)
      .subscribe(r => {
        // TODO: Remove in future, use websocks to refresh feedback
        window.location.reload();
      });
  }

  ngOnInit() {
    this.authService.GetAuthDetails().subscribe(details => {
      this.isLoggedIn = details.IsAuthenticated;
    });

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
            this.postDetails = response;
            if (this.postDetails.reasons) {
              this.postDetails.reasons.sort((left, right) => {
                if (left.tripped && !right.tripped) {
                  return -1;
                } else if (!left.tripped && right.tripped) {
                  return 1;
                }

                if (left.confidence && !right.confidence) {
                  return -1;
                } else if (!left.confidence && right.confidence) {
                  return 1;
                }
                return right.confidence - left.confidence;
              });
            }
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

            if (this.postDetails.tabTitle && this.postDetails.tabTitle !== '') {
              this.metaDataService.setTitle(`${this.postDetails.tabTitle} - ${this.postDetails.title}`);
            }
            if (this.postDetails.favIcon && this.postDetails.favIcon !== '') {
              this.metaDataService.setFavIcon(this.postDetails.favIcon);
            }
          }
        });
      }
    });
  }
}
