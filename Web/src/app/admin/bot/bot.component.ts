import { Component, OnInit } from '@angular/core';
import { AdminService, BotResponse } from '../../../swagger-gen';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { IMultiSelectTexts, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';

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

  public conflictDropdownSettings: IMultiSelectSettings;
  public conflictDropdownTexts: IMultiSelectTexts;

  private newFeedbackItemId = -1;
  private newConflictItemId = -1;

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

  public getConflictOptions() {
    return this.botDetails.feedbacks.map(r => ({
      id: r.id,
      name: r.name
    }));
  }

  public addNewFeedback() {
    this.botDetails.feedbacks.push({ id: this.newFeedbackItemId-- });
  }

  public addNewConflict() {
    this.botDetails.conflictExceptions.push({ id: this.newConflictItemId-- });
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (!id) {
        this.botDetails = {
          feedbacks: [],
          conflictExceptions: []
        };
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

    this.conflictDropdownSettings = {
      checkedStyle: 'fontawesome',
      buttonClasses: 'btn btn-default',
      dynamicTitleMaxItems: 10
    };
    this.conflictDropdownTexts = {
      defaultTitle: 'Select reasons'
    };
  }
}
