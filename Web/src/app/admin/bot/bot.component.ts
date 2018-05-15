import { Component, OnInit } from '@angular/core';
import { AdminService, BotResponse } from '../../../swagger-gen';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-bot',
  templateUrl: './bot.component.html',
  styleUrls: ['./bot.component.scss']
})
export class BotComponent implements OnInit {
  private botId: number | undefined;
  public isAdmin = false;
  public isNew = false;
  public submitted = false;

  constructor(
    private route: ActivatedRoute,
    private adminService: AdminService,
    private authService: AuthService,
    private router: Router) { }

  public botDetails: Partial<BotResponse>;

  public onSubmit() {
    this.submitted = true;
    if (this.isNew) {
      const createBotDetails = { ...this.botDetails as BotResponse, secret: (this.botDetails as any).secret };
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

    this.authService.GetAuthDetails().subscribe(details => {
      this.isAdmin = details.HasScope('admin');
    });
  }
}
