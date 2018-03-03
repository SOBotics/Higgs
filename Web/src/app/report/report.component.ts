import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TimeAgoPipe } from '../pipes/TimeAgoPipe';
import { ReviewerService, ReviewerReportResponse } from '../../swagger-gen';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  public postDetails: ReviewerReportResponse;
  constructor(private route: ActivatedRoute, private reviewerService: ReviewerService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const reportId = +params['id'];
      this.reviewerService.reviewerReportGet(reportId).subscribe(response => {
        this.postDetails = response;
      });
    });
  }
}
