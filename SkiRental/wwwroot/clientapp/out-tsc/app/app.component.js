var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { SkiService } from './skiService';
var AppComponent = /** @class */ (function () {
    function AppComponent(skiService) {
        this.skiService = skiService;
        this.loading = false;
        this.skis = [];
        this.title = 'Welcome to Angular';
        this.subtitle = '.NET Core + Angular CLI v7 + Bootstrap & FontAwesome + Swagger Template';
    }
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.loading = true;
        this.skiService.getSkis().subscribe(function (skis) {
            _this.loading = false;
            _this.skis = skis;
        });
    };
    AppComponent = __decorate([
        Component({
            selector: 'app-root',
            templateUrl: './app.component.html'
            //styleUrls: ['./app.component.css']
        }),
        __metadata("design:paramtypes", [SkiService])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map