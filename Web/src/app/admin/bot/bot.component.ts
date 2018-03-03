import { Component, OnInit } from '@angular/core';
import { AdminService, BotResponse } from '../../../swagger-gen';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bot',
  templateUrl: './bot.component.html',
  styleUrls: ['./bot.component.scss']
})
export class BotComponent implements OnInit {
  private botId: number | undefined;
  private isNew = false;
  constructor(private route: ActivatedRoute, private adminService: AdminService) { }

  public botDetails: Partial<BotResponse>;

  public onSubmit() {
    if (this.isNew) {
      this.adminService.adminRegisterBotPost(this.botDetails as BotResponse)
        .subscribe(a => { });
    } else {
      const updatedDetails = { ...this.botDetails as BotResponse, botId: this.botId as number };
      this.adminService.adminEditBotPost(updatedDetails)
        .subscribe(a => { });
    }
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (!id) {
        this.botDetails = {};
        this.isNew = true;
      } else {
        this.botId = +id;
        this.isNew = false;
        this.adminService.adminBotGet(this.botId).subscribe(response => {
          this.botDetails = response;
        });
      }
    });
  }
}
