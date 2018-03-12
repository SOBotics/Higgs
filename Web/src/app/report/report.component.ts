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
            if (response.dashboardName && dashboardName !== response.dashboardName) {
              this.router.navigateByUrl(`/${response.dashboardName}/report/${reportId}`, {
                skipLocationChange: true
              });
            }

            if (this.postDetails.tabTitle && this.postDetails.tabTitle !== '') {
              this.metaDataService.setTitle(this.postDetails.tabTitle);
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
