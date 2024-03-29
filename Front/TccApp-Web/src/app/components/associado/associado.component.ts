import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Associado } from '@app/models/Associado';
import { Pagination, PaginatedResult } from '@app/models/Pagination';
import { AssociadoService } from '@app/services/associado.service';
import { OrigemCadastroType, StatusCadastroType } from '@app/util/enums';
import { environment } from '@environments/environment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-associado',
  templateUrl: './associado.component.html'
})
export class AssociadoComponent implements OnInit {
  modalRef: BsModalRef;

  public associados: Associado[] = [];
  public associadoId = '';
  public pagination = {} as Pagination;
  public exibirImagem = true;

  public larguraImagem = 150;
  public margemImagem = 2;


  statusCadastroTypes = [
    { text: 'Aprovado', value: StatusCadastroType.Aprovado, color: 'bg-green', icon: 'pending' },
    { text: 'Cancelado', value: StatusCadastroType.Cancelado, color: 'bg-red', icon: 'done' },
    { text: 'Pré Cadastro', value: StatusCadastroType.PreCadastro, color: 'bg-orange', icon: 'close' }
  ]
  
  origemCadastroTypes = [
    { text: 'App', value: OrigemCadastroType.App, color: 'bg-green' },
    { text: 'Sistema Web', value: OrigemCadastroType.SistemaWeb, color: 'bg-red' },
  ]


  constructor(
    private associadoService: AssociadoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 1,
    } as Pagination;

    this.carregarAssociados();
  }

  termoBuscaChanged: Subject<string> = new Subject<string>();

  public filtrarAssociados(evt: any): void {
    if (this.termoBuscaChanged.observers.length === 0) {
      this.termoBuscaChanged
        .pipe(debounceTime(1000))
        .subscribe((filtrarPor) => {
          this.spinner.show();
          this.associadoService
            .getAssociados(
              this.pagination.currentPage,
              this.pagination.itemsPerPage,
              filtrarPor
            )
            .subscribe(
              (paginatedResult: PaginatedResult<Associado[]>) => {
                this.associados = paginatedResult.result;
                this.pagination = paginatedResult.pagination;
              },
              (error: any) => {
                this.spinner.hide();
                this.toastr.error('Erro ao Carregar os Associados', 'Erro!');
              }
            )
            .add(() => this.spinner.hide());
        });
    }
    this.termoBuscaChanged.next(evt.value);
  }

  public alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }

  public getImagemURL(imagemName: string): string {
    if (imagemName)
      return environment.apiURL + `resources/perfil/${imagemName}`;
    else
      return './assets/img/perfil.png';
  }

  public carregarAssociados(): void {
    this.spinner.show();

    this.associadoService
      .getAssociados(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (paginatedResult: PaginatedResult<Associado[]>) => {
          this.associados = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
        },
        (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao Carregar', 'Erro!');
        }
      )
      .add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, associadoId: string): void {
    event.stopPropagation();
    this.associadoId = associadoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.associadoService.deleteAssociado(this.associadoId)
      .subscribe(
        (result: any) => {
          if (result.message === 'Deletado') {
            this.toastr.success(
              'O Associado foi deletado com Sucesso.',
              'Deletado!'
            );
            this.carregarAssociados();
          }
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(
            `Erro ao tentar deletar o associado ${this.associadoId}`,
            'Erro'
          );
        }
      )
      .add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef.hide();
  }

  detalheAssociado(id: string): void {
    this.router.navigate([`associados/detalhe/${id}`]);
  }

  getStatusCadastroTypeNome(tipo: StatusCadastroType): string {
    return this.statusCadastroTypes.find(x => x.value === tipo)?.text;
  }
  getStatusCadastroTypeColor(tipo: StatusCadastroType): string {
    return this.statusCadastroTypes.find(x => x.value === tipo)?.color;
  }
  getOrigemCadastroTypeNome(tipo: OrigemCadastroType): string {
    return this.origemCadastroTypes.find(x => x.value === tipo)?.text;
  }
  getOrigemCadastroTypeColor(tipo: OrigemCadastroType): string {
    return this.origemCadastroTypes.find(x => x.value === tipo)?.color;
  }
}
