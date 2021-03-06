import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { environment } from '../environments/environment';
import { Params, ActivatedRoute, Router } from '@angular/router';
import { MetaDataService } from './services/meta-data.service';
import { ReportComponent } from './report/report.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public isLoggedIn: boolean;
  public isAdmin: boolean;
  public isDev: boolean;
  public isBotOwner: boolean;
  public isReviewer: boolean;
  public rawToken: string;
  private userName: string;
  public revision: string;
  constructor(
    private http: HttpClient,
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
    this.metaDataService.setFavIcon('/assets/favicon.ico');
  }

  ngOnInit() {
    this.http.get('/assets/revision.txt', { responseType: 'text' }).subscribe(a => this.revision = a);

    // Watch for access token in the URL. If it's there, store the token and remove it from the URL.
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      const accessToken = params['access_token'];
      if (accessToken) {
        this.authService.Login(accessToken);
        const fixedUrl = this.router.url.substr(0, this.router.url.indexOf('?'));
        this.router.navigateByUrl(fixedUrl);
      }
    });

    this.authService.GetAuthDetails().subscribe(details => {
      this.rawToken = `Bearer ${details.RawToken}`;
      this.isLoggedIn = details.IsAuthenticated;
      this.isAdmin = details.HasScope('admin');
      this.isDev = details.HasScope('dev');
      this.isBotOwner = details.HasScope('bot_owner');
      this.isReviewer = details.HasScope('reviewer');
      if (this.isLoggedIn) {
        this.userName = details.TokenData.unique_name;
      }
    });
  }

  private onLogoutClicked() {
    this.authService.Logout();
  }
}
