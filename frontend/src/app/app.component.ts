import { Component } from '@angular/core';
import { TimeLogTableComponent } from './time-log-table/time-log-table.component';
import { UserChartComponent } from './user-chart/user-chart.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: true,
  imports: [TimeLogTableComponent, UserChartComponent]
})
export class AppComponent {
  title = 'frontend';
}
