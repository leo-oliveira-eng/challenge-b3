export interface SimulateResponse {
  initialAmount: number;
  grossAmount: number;
  netAmount: number;
  taxAmount: number;
  taxRate: number;
  duration: number;
  monthlyRate: number;
  totalInterest: number;
}