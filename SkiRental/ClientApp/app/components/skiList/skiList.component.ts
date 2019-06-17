import { Component, Input } from "@angular/core";
import { SkiService, Ski, SkiRentCost } from "ClientApp/app/skiService";

const mockSkis = [
    { id: 1, name: 'ski 1', rate: 9.5, rentTime: null, customerName: null },
    { id: 2, name: 'ski 2', rate: 7.7, rentTime: null, customerName: null }
]

@Component({
    selector: 'ski-list',
    templateUrl: 'skiList.component.html',
    styleUrls: ['skiList.component.css']
})
export class SkiListComponent {
    @Input() skis = [];

    get hasSkis() { return !!this.skis && this.skis.length > 0; }

    constructor(public skiService: SkiService) { }
    
    rent(ski: Ski) {
        const name = prompt("Please enter customer name");
        if (!name) {
            return;
        }

        this.skiService.rentSki(ski.id, name).subscribe((_) => {});
    }

    return(ski: Ski) {
        this.skiService.returnSki(ski.id).subscribe((rentCost: SkiRentCost) => {
            alert(`Rent cost for customer ${rentCost.customerName} is: ${rentCost.rentCost.toFixed(2)}`);
        });
    }
}