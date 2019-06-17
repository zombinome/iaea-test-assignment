import { Component } from "@angular/core";
import { SkiService } from "ClientApp/app/skiService";
import { tap } from "rxjs/operators";

@Component({
    selector: 'register-ski',
    templateUrl: 'registerSki.component.html'
})
export class RegisterSkiComponent {
    model = new RegistrationModel();

    disabled: boolean = false;

    constructor(private skiService: SkiService) {
    }

    get isModelValid() {
        return !!this.model.name && this.model.rate >= 0;
    }

    registerSki() {
        this.disabled = true;
        this.skiService
            .registerSki(this.model.name, this.model.rate)
            .subscribe((_) => {
                this.model.name = '';
                this.model.rate = 0;
                this.disabled = false;
            });
    }
}

class RegistrationModel {
    name: string = '';
    rate: number = 0.0;
}