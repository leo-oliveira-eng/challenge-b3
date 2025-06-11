import { Component, OnInit } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { SimulateRequest } from '../Shared/SimulateRequest';
import { CdbYieldSimulatorService } from '../Shared/services/cdb-yield-simulator.service';
import { SimulateResponse } from '../Shared/SimulateResponse';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { NgChartsModule } from 'ng2-charts';
import { ChartData, ChartOptions } from 'chart.js';

@Component({
  selector: 'app-simulate-investment',
  templateUrl: './simulate-investment.component.html',
  imports: [
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientModule,
    CommonModule,
    NgChartsModule,
  ],
  standalone: true,
})
export class SimulateInvestmentComponent implements OnInit {
  form!: FormGroup;
  result?: SimulateResponse;
  pieChart: any;
  constructor(
    private fb: FormBuilder,
    private simulatorService: CdbYieldSimulatorService
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      amount: [null, [Validators.required, Validators.min(0)]],
      interestRate: [{ value: 0.009, disabled: true }],
      duration: [null, [Validators.required, Validators.min(0)]],
      taxRate: [{ value: 1.08, disabled: true }],
    });
  }

  onSubmit() {
    if (this.form.valid) {
      const formData = this.form.getRawValue();
      const request: SimulateRequest = {
        amount: formData.amount,
        duration: formData.duration,
      };

      this.simulatorService.simulate(request).subscribe({
        next: (response) => {
          this.result = response.data?.value;
          console.log('Simulation result:', this.result);
          this.pieChartData.datasets[0].data = [
            this.result?.initialAmount ?? 0,
            this.result?.totalInterest ?? 0,
          ];
        },
        error: (error) => {
          console.error('Error occurred during simulation:', error);
        },
      });
    } else {
      console.error('Form is invalid', this.form.errors);
      Object.entries(this.form.controls).forEach(([key, control]) => {
        if (control.invalid) {
          console.error(`Campo "${key}" inválido:`, control.errors);
        }
      });
    }
  }

  clear() {
    this.ngOnInit();
  }

  resetForm() {
    this.form = this.fb.group({
      amount: [null, [Validators.required, Validators.min(0)]],
      interestRate: [{ value: 0.9, disabled: true }],
      duration: [null, [Validators.required, Validators.min(0)]],
      taxRate: [{ value: 108, disabled: true }],
    });
  }

  doAnotherSimulation() {
    this.result = undefined;
    this.clear();
  }

  pieChartType: 'pie' = 'pie';

  pieChartData: ChartData<'pie', number[], string | string[]> = {
    labels: ['Investido', 'Juros'],
    datasets: [
      {
        data: [0, 0],
        backgroundColor: ['#4F8AFF', '#43A047'],
      },
    ],
  };

  pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    plugins: {
      legend: {
        position: 'bottom',
      },
    },
  };
}
