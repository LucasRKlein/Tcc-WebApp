import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Associado } from '@app/models/Associado';
import { Pagination, PaginatedResult } from '@app/models/Pagination';
import { AssociadoService } from '@app/services/associado.service';
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
  public associadoId = 0;
  public pagination = {} as Pagination;

  public larguraImagem = 150;
  public margemImagem = 2;
  public exibirImagem = true;

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

  constructor(
    private associadoService: AssociadoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3,
      totalItems: 1, 
    } as Pagination;

    this.carregarAssociados();
  }

  public alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }

  public mostraImagem(imagemURL: string): string {
    return imagemURL !== ''
      ? `${environment.apiURL}resources/images/${imagemURL}`
      : 'assets/img/semImagem.jpeg';
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
          this.toastr.error('Erro ao Carregar os Associados', 'Erro!');
        }
      )
      .add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, associadoId: number): void {
    event.stopPropagation();
    this.associadoId = associadoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public pageChanged(event): void {
    this.pagination.currentPage = event.page;
    this.carregarAssociados();
  }

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.associadoService
      .deleteAssociado(this.associadoId)
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

  detalheAssociado(id: number): void {
    this.router.navigate([`associados/detalhe/${id}`]);
  }
}
