import { BASE_URL } from "./helpers/app";
import * as chai from "chai";

describe("/customers", () => {
    it("should get 100 by default", async () => {
        const response = await chai.request(BASE_URL)
            .get("customers")
            .set("Accept", "application/json")
            .send();

        chai.expect(response.body).to.be.array()
            .and.have.length(100);
    });

    it("should respect take", async () => {
        const response = await chai.request(BASE_URL)
            .get("customers?take=5")
            .set("Accept", "application/json")
            .send();

        chai.expect(response.body).to.be.array()
            .and.have.length(5);
    });

    it("should have first and last name", async () => {
        const response = await chai.request(BASE_URL)
            .get("customers?take=5")
            .set("Accept", "application/json")
            .send();

        chai.expect(response.body).to.be.array();

        response.body.forEach((customer: any) => {
            chai.expect(customer).to.have.property("firstName").of.string;
            chai.expect(customer).to.have.property("lastName").of.string;
        });
    });
});
