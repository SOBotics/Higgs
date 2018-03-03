import { Component, OnInit } from '@angular/core';
import { ReviewerService, ReviewerReportsResponse } from '../../swagger-gen';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {

  public reportsResponse: ReviewerReportsResponse[] = [];
  constructor(private reviewerService: ReviewerService) { }

  ngOnInit() {
    this.reviewerService.reviewerReportsGet()
      .subscribe(response => this.reportsResponse = response);
  }
}
