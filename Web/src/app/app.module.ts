import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AdminService, BASE_PATH, Configuration, ReviewerService, AuthenticationService, AnalyticsService } from '../swagger-gen';
import { environment } from '../environments/environment';
import { AuthConfiguration } from './AuthConfiguration';
import { RouterModule } from '@angular/router';
import { appRoutes } from './app.routes';
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
import { ReportsComponent } from './reports/reports.component';
import { MetaDataService } from './services/meta-data.service';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { MiniProfilerUiResultComponent } from './mini-profiler/mini-profiler-ui-result/mini-profiler-ui-result.component';
import { MiniProfilerUiComponent } from './mini-profiler/mini-profiler-ui/mini-profiler-ui.component';
import { HttpRequestInterceptorService } from './mini-profiler/http-request-interceptor.service';
import { UsersComponent } from './admin/users/users.component';
import { UserComponent } from './admin/user/user.component';
import { ChartModule } from 'angular-highcharts';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { ReportPageComponent } from './report-page/report-page.component';
import { ReviewComponent } from './review/review.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ReportComponent,
    TimeAgoPipe,
    OrderByPipe,
    BotComponent,
    BotsComponent,
    AdminHomeComponent,
    ReportsComponent,
    PageNotFoundComponent,
    MiniProfilerUiComponent,
    MiniProfilerUiResultComponent,
    UsersComponent,
    UserComponent,
    ReportPageComponent,
    ReviewComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ChartModule,
    MultiselectDropdownModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    MetaDataService,
    AdminService,
    ReviewerService,
    AuthenticationService,
    AnalyticsService,
    { provide: BASE_PATH, useValue: environment.apiHost },
    { provide: Configuration, useClass: AuthConfiguration },
    AuthService,
    HttpRequestInterceptorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
