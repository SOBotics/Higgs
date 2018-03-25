import { Component, OnInit, QueryList, ViewChildren, AfterViewInit, ContentChildren } from '@angular/core';
import { AdminService } from '../../../swagger-gen';
import { ActivatedRoute } from '@angular/router';
import 'rxjs/add/operator/combineLatest';

@Component({
  selector: 'app-bot-scope',
  templateUrl: './bot-scope.component.html',
  styleUrls: ['./bot-scope.component.scss']
})
export class BotScopeComponent implements OnInit {
  private botId: number;
  public scopes: { Name: string, Assigned: boolean }[];

  constructor(private adminService: AdminService, private route: ActivatedRoute) { }

  ngOnInit() {
    const allScopes = this.adminService.adminScopesGet();
    this.route.params.subscribe(params => {
      this.botId = +params['id'];

      const botScopes = this.adminService.adminBotScopesGet(this.botId);

      allScopes.combineLatest(botScopes, (s1, s2) => {
        return {
          AllScopes: s1,
          BotScopes: s2
        };
      }).subscribe(stream => {
        this.scopes = [];
        for (let i = 0; i < stream.AllScopes.length; i++) {
          const scopeName = stream.AllScopes[i];
          this.scopes.push({ Name: scopeName, Assigned: stream.BotScopes.indexOf(scopeName) !== -1 });
        }
      });
    });
  }

  public async updateScopes() {
    const assignedScopes = this.scopes.filter(scope => scope.Assigned)
      .map(scope => scope.Name);

    this.adminService.adminSetBotScopesPost({
      botId: this.botId,
      scopes: assignedScopes
    })
      .subscribe(a => window.location.reload());
  }
}
