import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TimeLogTableComponent } from './time-log-table/time-log-table.component';
import { UserChartComponent } from './user-chart/user-chart.component';

const routes: Routes = [
  { path: 'time-logs', component: TimeLogTableComponent },
  { path: 'user-chart', component: UserChartComponent },
  { path: '', redirectTo: '/time-logs', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
