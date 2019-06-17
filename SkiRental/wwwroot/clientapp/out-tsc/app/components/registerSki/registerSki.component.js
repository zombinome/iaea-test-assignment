var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from "@angular/core";
import { SkiService } from "ClientApp/app/skiService";
var RegisterSkiComponent = /** @class */ (function () {
    function RegisterSkiComponent(skiService) {
        this.skiService = skiService;
        this.model = new RegistrationModel();
        this.disabled = false;
    }
    Object.defineProperty(RegisterSkiComponent.prototype, "isModelValid", {
        get: function () {
            return !!this.model.name && this.model.rate >= 0;
        },
        enumerable: true,
        configurable: true
    });
    RegisterSkiComponent.prototype.registerSki = function () {
        var _this = this;
        this.disabled = true;
        this.skiService
            .registerSki(this.model.name, this.model.rate)
            .subscribe(function (_) {
            _this.model.name = '';
            _this.model.rate = 0;
            _this.disabled = false;
        });
    };
    RegisterSkiComponent = __decorate([
        Component({
            selector: 'register-ski',
            templateUrl: 'registerSki.component.html'
        }),
        __metadata("design:paramtypes", [SkiService])
    ], RegisterSkiComponent);
    return RegisterSkiComponent;
}());
export { RegisterSkiComponent };
var RegistrationModel = /** @class */ (function () {
    function RegistrationModel() {
        this.name = '';
        this.rate = 0.0;
    }
    return RegistrationModel;
}());
//# sourceMappingURL=registerSki.component.js.map