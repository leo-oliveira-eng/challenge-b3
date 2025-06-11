import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SimulateInvestmentComponent } from './simulador-cdb/simulate-investment/simulate-investment.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SimulateInvestmentComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'cdb-yield-simulator';
}
