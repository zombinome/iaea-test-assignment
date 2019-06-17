var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input } from "@angular/core";
import { SkiService } from "ClientApp/app/skiService";
var mockSkis = [
    { id: 1, name: 'ski 1', rate: 9.5, rentTime: null, customerName: null },
    { id: 2, name: 'ski 2', rate: 7.7, rentTime: null, customerName: null }
];
var SkiListComponent = /** @class */ (function () {
    function SkiListComponent(skiService) {
        this.skiService = skiService;
        this.skis = [];
    }
    Object.defineProperty(SkiListComponent.prototype, "hasSkis", {
        get: function () { return !!this.skis && this.skis.length > 0; },
        enumerable: true,
        configurable: true
    });
    SkiListComponent.prototype.rent = function (ski) {
        var name = prompt("Please enter customer name");
        if (!name) {
            return;
        }
        this.skiService.rentSki(ski.id, name).subscribe(function (_) { });
    };
    SkiListComponent.prototype.return = function (ski) {
        this.skiService.returnSki(ski.id).subscribe(function (rentCost) {
            alert("Rent cost for customer " + rentCost.customerName + " is: " + rentCost.rentCost.toFixed(2));
        });
    };
    __decorate([
        Input(),
        __metadata("design:type", Object)
    ], SkiListComponent.prototype, "skis", void 0);
    SkiListComponent = __decorate([
        Component({
            selector: 'ski-list',
            templateUrl: 'skiList.component.html',
            styleUrls: ['skiList.component.css']
        }),
        __metadata("design:paramtypes", [SkiService])
    ], SkiListComponent);
    return SkiListComponent;
}());
export { SkiListComponent };
//# sourceMappingURL=skiList.component.js.map