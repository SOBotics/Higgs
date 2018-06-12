import { Component, OnInit } from '@angular/core';
import { ReviewerService, PagingResponseReviewerReportsResponse } from '../../swagger-gen';
import { ActivatedRoute, Router } from '@angular/router';
import { GetPagingInfo } from '../../utils/PagingHelper';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {

  public reportsResponse: PagingResponseReviewerReportsResponse = null;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private reviewerService: ReviewerService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const pageNumber = +params['pageNumber'] || 1;
      this.reviewerService.reviewerReportsGet(pageNumber, 50)
        .subscribe(response => this.reportsResponse = response);
    });
  }

  public getPages() {
    return GetPagingInfo(this.reportsResponse);
  }

  public loadPage(pageNumber: number) {
    this.router.navigateByUrl(`/reports?pageNumber=${pageNumber}`);
  }
}
