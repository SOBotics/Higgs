import { Component, OnInit } from '@angular/core';
import { AdminService, ViewBotFeedbackTypesResponse } from '../../../swagger-gen';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-bot-feedback-types',
  templateUrl: './bot-feedback-types.component.html',
  styleUrls: ['./bot-feedback-types.component.scss']
})
export class BotFeedbackTypesComponent implements OnInit {

  private botId: number;
  private feedbacks: ViewBotFeedbackTypesResponse[];
  private submitted = false;
  constructor(private adminService: AdminService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.botId = +id;
      if (!isNaN(this.botId)) {
        this.adminService.adminViewBotFeedbackTypesGet(this.botId).subscribe(response => {
          this.feedbacks = response;
        });
      }
    });
  }

  public addNew() {
    this.feedbacks.push({ id: 0 });
  }

  public onSubmit() {
    this.submitted = true;
    this.adminService.adminEditBotFeedbackTypesPost({
      botId: this.botId,
      feedbackTypes: this.feedbacks
    }).subscribe(a => this.router.navigateByUrl('/admin/bots'));
  }
}
