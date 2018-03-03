import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
import { OrderByPipe } from './pipes/OrderByPipe';
import { BotComponent } from './admin/bot/bot.component';
import { BotsComponent } from './admin/bots/bots.component';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';


@NgModule({
  declarations: [
    AppComponent,
    OAuthComponent,
    HomeComponent,
    ReportComponent,
    TimeAgoPipe,
    OrderByPipe,
    BotComponent,
    BotsComponent,
    AdminHomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
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
