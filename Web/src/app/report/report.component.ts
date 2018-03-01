import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TimeAgoPipe } from '../pipes/TimeAgoPipe';
import { ReviewerService, ReportResponse } from '../../swagger-gen';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  private postDetails: ReportResponse;
  constructor(private route: ActivatedRoute, private reviewerService: ReviewerService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const reportId = +params['id'];
      this.reviewerService.reviewerGetReportGet(reportId).subscribe(response => {
        this.postDetails = response;
        this.postDetails.feedback = [{
          userName: 'Rob',
          icon: '✓',
          colour: 'green'
        }, {
          userName: 'NotRob',
          icon: '✗',
          colour: 'red'
        }];
        this.postDetails.contentFragments = [{
          id: 5,
          name: 'Original',
          content: 'Some content',
          order: 0
        }, {
          id: 7,
          name: 'Obfuscated',
          content: 'Blah',
          order: 1
        }];

        this.postDetails.allowedFeedback = [{
          name: 'tp',
          colour: 'green'
        }];
      });
    });
  }
}
