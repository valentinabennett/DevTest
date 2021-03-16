export class CustomerModel {
    constructor(name: string, type: string) {
        this.name = name;
        this.type = type;
    }
    id: string;
    name: string;
    type: string;
}
