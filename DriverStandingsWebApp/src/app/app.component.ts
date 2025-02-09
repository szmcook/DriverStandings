import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DriverStandingsComponent } from './driver-standings/driver-standings.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, DriverStandingsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'DriverStandingsWebApp';
}
