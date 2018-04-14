import { Routes, UrlSegment } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ReportComponent } from './report/report.component';
import { BotComponent } from './admin/bot/bot.component';
import { BotsComponent } from './admin/bots/bots.component';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';
import { ReportsComponent } from './reports/reports.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { BotScopesComponent } from './admin/bot-scopes/bot-scopes.component';
import { BotScopeComponent } from './admin/bot-scope/bot-scope.component';
import { BotFeedbackTypesComponent } from './admin/bot-feedback-types/bot-feedback-types.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },

    { path: 'admin', component: AdminHomeComponent },
    { path: 'admin/bots', component: BotsComponent },
    { path: 'admin/bot/feedbackTypes/:id', component: BotFeedbackTypesComponent },
    { path: 'admin/botScopes', component: BotScopesComponent },
    { path: 'admin/botScope/:id', component: BotScopeComponent },
    { path: 'admin/bot', component: BotComponent },
    { path: 'admin/bot/:id', component: BotComponent },

    { path: 'reports', component: ReportsComponent },
    { path: 'report/:id', component: ReportComponent },

    { path: ':dashboardName/report/:id', component: ReportComponent },

    { path: '**', component: PageNotFoundComponent }
];
