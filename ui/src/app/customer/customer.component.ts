import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CustomerService } from '../services/customer.service';
import { CustomerModel } from '../models/customer.model';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-customer',
    templateUrl: './customer.component.html',
    styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit, OnDestroy {
    types: string[] = [];
    customers: CustomerModel[] = [];
    $subcriptions: Subscription[] = [];
    public newCustomer: CustomerModel = {
        id: null,
        name: null,
        type: null
    };

    constructor(private customerService: CustomerService) {}

    ngOnInit() {
        this.$subcriptions.push(this.customerService.GetCustomerTypes().subscribe(data => (this.types = [...data])));
        this.$subcriptions.push(
            this.customerService.GetCustomers().subscribe(customers => {
                this.customers = [...customers];
            })
        );
    }

    public createCustomer(form: NgForm): void {
        if (form.invalid) {
            alert('form is not valid');
        } else {
            this.$subcriptions.push(
                this.customerService.CreateCustomer(this.newCustomer).subscribe(() => {
                    this.customerService.GetCustomers().subscribe(customers => (this.customers = [...customers]));
                })
            );
        }
    }

    ngOnDestroy() {
        if (this.$subcriptions) {
            this.$subcriptions.forEach(x => x.unsubscribe);
        }
    }
}
