import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TimeAgoPipe } from '../pipes/MessageTimePipe';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  private postDetails = {
    detectionScore: 7.5,
    botLogo: 'https://i.stack.imgur.com/pYTTv.png',
    botName: 'Heat Detector',
    authorPrevious: 1,
    title: 'Kdmf driver example on windows 8.1',
    details: 'Some extra details about the report here (think the edit vandalism bot; describing which revision)',
    link: 'https://www.stackoverflow.com/a/48841057',
    reportedText: '<p>Did you manage to solve this problem?</p>',
    postedDate: new Date('2018-01-05T09:00'),
    reportedDate: new Date('2018-01-05T09:01'),
    authorName: 'Rob',
    authorLink: 'https://stackoverflow.com/users/563532/rob',
    reasons: [
      {
        reasonId: 1,
        name: 'Regex - /so[m]ecr?^azyex[(pres)|(sion)]here',
        confidence: 8,
        seen: 76,
      },
      {
        reasonId: 2,
        name: 'NaiveBayes',
        confidence: 1,
        seen: 503,
      },
      {
        reasonId: 3,
        name: 'OpenLPM',
        confidence: 2,
        seen: 354
      }
    ]
  };

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const reportId = +params['id'];
    });
  }
}
