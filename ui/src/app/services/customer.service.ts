import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomerModel } from '../models/customer.model';

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    constructor(private httpClient: HttpClient) {}

    public GetCustomerTypes(): Observable<string[]> {
        return this.httpClient.get<string[]>('https://localhost:5001/customers/customerTypes');
    }

    public GetCustomers(): Observable<CustomerModel[]> {
        return this.httpClient.get<CustomerModel[]>('https://localhost:5001/customers');
    }

    public GetCustomer(customerId: string): Observable<CustomerModel> {
        return this.httpClient.get<CustomerModel>(`https://localhost:5001/customers/${customerId}`);
    }

    public CreateCustomer(customer: CustomerModel): Observable<CustomerModel> {
        return this.httpClient.post<CustomerModel>('https://localhost:5001/customers', customer);
    }
}
