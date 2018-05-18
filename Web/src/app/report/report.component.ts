import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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
  @Input()
  public postDetails: ReviewerReportResponse;

  public isLoggedIn: boolean;
  public isRoomOwner: boolean;
  public myUserId: number;
  public reportNotFound: boolean;

  @Output()
  public FeedbackClicked = new EventEmitter<number>();
  @Output()
  public FeedbackCleared = new EventEmitter<void>();

  constructor(
    private authService: AuthService,
    private reviewerService: ReviewerService,
  ) { }

  public feedbackClicked(id: number) {
    this.reviewerService.reviewerSendFeedbackPost({ reportId: this.postDetails.id, feedbackId: id})
      .subscribe(r => {
        this.FeedbackClicked.next(id);
      });
  }

  ngOnInit() {
    this.authService.GetAuthDetails().subscribe(details => {
      this.isLoggedIn = details.IsAuthenticated;
      this.isRoomOwner = details.HasScope('room_owner');
      const userIdStr = details.GetClaim('accountId');
      if (userIdStr) {
        this.myUserId = parseInt(userIdStr, 10);
      }
    });
  }

  public clearFeedback(feedbackId: number) {
    this.reviewerService.reviewerClearFeedbackPost({ feedbackId: feedbackId })
    .subscribe(r => {
      this.FeedbackCleared.next();
    });
  }
}
