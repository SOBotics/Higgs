import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private reviewerService: ReviewerService,
    private metaDataService: MetaDataService
  ) { }

  ngOnInit() {
    this.authService.GetAuthDetails().subscribe(details => {
      this.isLoggedIn = details.IsAuthenticated;
    });

    this.route.params.subscribe(params => {
      const reportId = +params['id'];
      this.reviewerService.reviewerReportGet(reportId).subscribe(response => {
        this.postDetails = response;
        if (this.postDetails.tabTitle) {
          this.metaDataService.setTitle(this.postDetails.tabTitle);
        }
        if (this.postDetails.favIcon) {
          this.metaDataService.setFavIcon(this.postDetails.favIcon);
        }

      });
    });
  }
}
