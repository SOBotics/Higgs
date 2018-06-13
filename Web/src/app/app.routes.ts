import { Routes, UrlSegment } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ReportComponent } from './report/report.component';
import { BotComponent } from './admin/bot/bot.component';
import { BotsComponent } from './admin/bots/bots.component';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';
import { ReportsComponent } from './reports/reports.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { UsersComponent } from './admin/users/users.component';
import { UserComponent } from './admin/user/user.component';
import { ReportPageComponent } from './report-page/report-page.component';
import { ReviewComponent } from './review/review.component';
import { DashboardComponent } from './dashboard/dashboard.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },

    { path: 'admin', component: AdminHomeComponent },
    { path: 'admin/bots', component: BotsComponent },
    { path: 'admin/bot', component: BotComponent },
    { path: 'admin/bot/:id', component: BotComponent },
    { path: 'admin/users', component: UsersComponent },
    { path: 'admin/user/:id', component: UserComponent },

    { path: 'review', component: ReviewComponent },

    { path: 'reports', component: ReportsComponent },
    { path: 'report/:id', component: ReportPageComponent },

    { path: ':dashboardName/report/:id', component: ReportPageComponent },

    { path: ':dashboardName', component: DashboardComponent },

    { path: '**', component: PageNotFoundComponent }
];
