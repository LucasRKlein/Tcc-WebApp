import { Injectable, Injector } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class UtilService {

  formatterNumberTwoDigits = new Intl.NumberFormat('pt-BR', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  });

  constructor(
  ) {
  }

  public newGuid(): string {
    let d = new Date().getTime();
    const uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c) => {
      const r = (d + Math.random() * 16) % 16 | 0; // tslint:disable-line
      d = Math.floor(d / 16);
      return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16); // tslint:disable-line
    });
    return uuid;
  }

  public getGuidSequencial(id: string) {
    let novoId = "00000000-0000-0000-0000-" + id.padStart(12, "0");
    return novoId;
  }

  public formatCpf(cpf: string) {
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4");
  }

  public formatCnpj(cnpj: string) {
    return cnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, "\$1.\$2.\$3\/\$4\-\$5");
  }

  public fromJsonDate(date: Date): string {
    if (!date) {
      return null;
    }
    return new Date(date).toISOString().substring(0, 10);
  }

  public fromJsonDateTime(date: Date): string {
    if (!date) {
      return null;
    }
    return new Date(date).toISOString().substring(0, 16);
  }
}
