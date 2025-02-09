import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { DriverStandingsService } from '../driver-standings.service';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { HttpClientModule } from '@angular/common/http';
import { DriverStanding } from '../driver-standing.interface';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

@Component({
  standalone: true,
  selector: 'app-driver-standings',
  imports: [
    CommonModule,
    MatTableModule,
    MatSortModule,
    MatInputModule,
    HttpClientModule,
    MatFormFieldModule,
    MatSelectModule,
  ],
  providers: [DriverStandingsService],
  templateUrl: './driver-standings.component.html',
  styleUrls: ['./driver-standings.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class DriverStandingsComponent implements OnInit {
  displayedColumns: string[] = ['position', 'name', 'season_points', 'season_team_name'];
  dataSource: MatTableDataSource<DriverStanding> = new MatTableDataSource();

  teamNames: string[] = [];
  countryCodes: string[] = [];
  teamFilter: string = '';
  countryCodeFilter: string = '';
  years: number[] = [];
  selectedYear: number = new Date().getFullYear()-1;

  @ViewChild(MatSort) sort!: MatSort;

  constructor(private driverStandingsService: DriverStandingsService) {}

  ngOnInit(): void {
    this.generateYears();

    this.getDriverStandings(this.selectedYear);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }
  
  generateYears() {
    // TODO this shouldn't be hardcoded
    const currentYear = new Date().getFullYear();
    this.years = Array.from({ length: currentYear - 2000 + 1 }, (_, i) => currentYear - i);
  }

  getDriverStandings(year: number){
    this.dataSource = new MatTableDataSource();
    this.driverStandingsService.getDriverStandings(year).subscribe(
      data => {
        this.setDataSource(data);
      },
      error => {
        console.error('Error fetching driver standings', error);
      }
    );
  }

  setDataSource(data: DriverStanding[]) {
    this.dataSource = new MatTableDataSource(data);
    this.dataSource.sort = this.sort;

    // Populate team names and country codes for filter dropdowns
    this.teamNames = ['All Teams', ...new Set(data.map(standing => standing.season_team_name))];
    this.countryCodes = ['All Countries', ...new Set(data.map(standing => standing.driver_country_code))];
  }

  onYearChange(event: any) {
    this.selectedYear = event.value;
    this.getDriverStandings(this.selectedYear);
  }

  // Apply filter based on dropdown selection (team or country)
  applyFilter(event: any, filterType: string) {
    const filterValue = event.value.trim().toLowerCase();

    console.log(filterValue)
    
    if (filterType === 'team') {
      this.teamFilter = filterValue === 'all teams' ? '' : filterValue;
    } else if (filterType === 'country') {
      this.countryCodeFilter = filterValue === 'all countries' ? '' : filterValue;
    }
  
    this.dataSource.filterPredicate = (data: DriverStanding) => {
      const teamMatches = this.teamFilter ? data.season_team_name.toLowerCase().includes(this.teamFilter) : true;
      const countryMatches = this.countryCodeFilter ? data.driver_country_code.toLowerCase().includes(this.countryCodeFilter) : true;
  
      return teamMatches && countryMatches;
    };
  
    // Trigger the filter update
    this.dataSource.filter = filterValue;
  }
  
  
}
