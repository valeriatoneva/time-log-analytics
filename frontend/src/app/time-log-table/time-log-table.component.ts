import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { TimeLog } from '../models/time-log.model';
import { TimeLogService } from '../time-log.service';
import { SharedModule } from '../shared/shared.module';

@Component({
  selector: 'app-time-log-table',
  templateUrl: './time-log-table.component.html',
  styleUrls: ['./time-log-table.component.scss'],
  standalone: true,
  imports: [SharedModule], 
})

export class TimeLogTableComponent implements AfterViewInit {
  displayedColumns: string[] = ['userName', 'email', 'project', 'date', 'hours'];
  dataSource = new MatTableDataSource<TimeLog>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private timeLogService: TimeLogService) {
    console.log('TimeLogTableComponent instantiated'); 
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.loadTimeLogs();
  }

  loadTimeLogs() {
    this.timeLogService.getTimeLogs(this.paginator.pageIndex, this.paginator.pageSize)
      .subscribe((response) => {
        this.dataSource.data = response.data;
        this.paginator.length = response.totalCount;
      });
  }
}
