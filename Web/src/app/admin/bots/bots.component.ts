import { Component, OnInit } from '@angular/core';
import {  ReviewerService, ReviewerDashboardsResponse } from '../../../swagger-gen';

@Component({
  selector: 'app-bots',
  templateUrl: './bots.component.html',
  styleUrls: ['./bots.component.scss']
})
export class BotsComponent implements OnInit {
  public botsResponse: ReviewerDashboardsResponse[] = [];
  constructor(private reviewerService: ReviewerService) { }

  ngOnInit() {
    this.reviewerService.reviewerDashboardsGet()
      .subscribe(response => this.botsResponse = response);
  }
}
