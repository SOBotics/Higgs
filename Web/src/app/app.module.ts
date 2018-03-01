import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AdminService, BASE_PATH, Configuration, ReviewerService } from '../swagger-gen';
import { environment } from '../environments/environment';
import { AuthConfiguration } from './AuthConfiguration';
import { OAuthComponent } from './oauth/oauth.component';
import { RouterModule } from '@angular/router';
import { appRouts } from './app.routes';
import { HomeComponent } from './home/home.component';
import { AuthService } from './services/auth.service';
import { ReportComponent } from './report/report.component';
import { TimeAgoPipe } from './pipes/TimeAgoPipe';
import { HttpClient } from 'selenium-webdriver/http';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    OAuthComponent,
    HomeComponent,
    ReportComponent,
    TimeAgoPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRouts)
  ],
  providers: [
    AdminService,
    ReviewerService,
    { provide: BASE_PATH, useValue: environment.apiHost },
    { provide: Configuration, useClass: AuthConfiguration },
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
