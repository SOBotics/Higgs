import { Component, OnInit } from '@angular/core';
import { ReviewerService, ReviewerReportResponse } from '../../swagger-gen';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {
  public reviewingReport: ReviewerReportResponse;
  public done = false;

  private currentSubscription: Subscription;

  constructor(private reviewerService: ReviewerService) { }

  ngOnInit() {
    this.currentSubscription = this.reviewerService.reviewerNextReviewGet().subscribe(response => {
      this.reviewingReport = response;
      if (!this.reviewingReport) {
        this.done = true;
      }
    });
  }

  public NextItem() {
    if (this.currentSubscription) {
      this.currentSubscription.unsubscribe();
    }
    this.currentSubscription = this.reviewerService.reviewerNextReviewGet(this.reviewingReport.id).subscribe(response => {
      this.reviewingReport = response;
      if (!this.reviewingReport) {
        this.done = true;
      }
    });
  }
  public OnFeedbackCleared() {
  }
}
