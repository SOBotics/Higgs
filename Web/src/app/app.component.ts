import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { environment } from '../environments/environment';
import { Params, ActivatedRoute, Router } from '@angular/router';
import { MetaDataService } from './services/meta-data.service';
import { ReportComponent } from './report/report.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public isLoggedIn: boolean;
  private userName: string;
  constructor(
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private metaDataService: MetaDataService
  ) {

  }

  public getLoginUrl() {
    return `${environment.apiHost}/Authentication/Login?redirect_uri=${window.location}&scope=all`;
  }

  public onRouterActivate(component: any) {
    if (component instanceof ReportComponent) {
      return;
    }
    this.metaDataService.setTitle('Higgs');
    this.metaDataService.setFavIcon('favicon.ico');
  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      const accessToken = params['access_token'];
      if (accessToken) {
        this.authService.Login(accessToken);
        const fixedUrl = this.router.url.substr(0, this.router.url.indexOf('?'));
        this.router.navigateByUrl(fixedUrl);
      }
    });

    this.authService.GetAuthDetails().subscribe(details => {
      this.isLoggedIn = details.IsAuthenticated;
      if (this.isLoggedIn) {
        this.userName = details.RawToken.unique_name;
      }
    });
  }

  private onLogoutClicked() {
    this.authService.Logout();
  }
}
