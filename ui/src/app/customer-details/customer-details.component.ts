import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../services/customer.service';
import { CustomerModel } from '../models/customer.model';

@Component({
    selector: 'app-customer-details',
    templateUrl: './customer-details.component.html'
})
export class CustomerDetailsComponent implements OnInit {
    private customerId: string;
    public customer: CustomerModel;

    constructor(route: ActivatedRoute, private customerService: CustomerService) {
        this.customerId = route.snapshot.params.id;
    }

    ngOnInit() {
        this.customerService.GetCustomer(this.customerId).subscribe(customer => (this.customer = customer));
    }
}
