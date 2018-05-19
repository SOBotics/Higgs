import { Component, OnInit } from '@angular/core';
import { ReviewerService, PagingResponseReviewerReportsResponse } from '../../swagger-gen';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {

  public reportsResponse: PagingResponseReviewerReportsResponse = null;
  constructor(private reviewerService: ReviewerService) { }

  ngOnInit() {
    this.reviewerService.reviewerReportsGet(1, 50)
      .subscribe(response => this.reportsResponse = response);
  }
}
