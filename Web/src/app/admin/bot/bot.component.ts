import { Component, OnInit } from '@angular/core';
import { AdminService, BotResponse } from '../../../swagger-gen';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-bot',
  templateUrl: './bot.component.html',
  styleUrls: ['./bot.component.scss']
})
export class BotComponent implements OnInit {
  private botId: number | undefined;
  private isNew = false;
  constructor(
    private route: ActivatedRoute,
    private adminService: AdminService,
    private router: Router) { }

  public botDetails: Partial<BotResponse>;

  public onSubmit() {
    if (this.isNew) {
      const createBotDetails = {...this.botDetails as BotResponse, secret: (this.botDetails as any).secret };
      this.adminService.adminRegisterBotPost(createBotDetails)
        .subscribe(a => this.router.navigateByUrl('/admin/bots'));
    } else {
      const updatedDetails = { ...this.botDetails as BotResponse, botId: this.botId as number };
      this.adminService.adminEditBotPost(updatedDetails)
        .subscribe(a => this.router.navigateByUrl('/admin/bots'));
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
