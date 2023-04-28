import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { LoteService } from './../../../services/lote.service';
import { EventoService } from '@app/services/evento.service';
import { Evento } from '@app/models/Evento';
import { Lote } from '@app/models/Lote';
import { DatePipe } from '@angular/common';
import { environment } from '@environments/environment';
import { Associado } from '@app/models/Associado';
import { AssociadoService } from '@app/services/associado.service';

@Component({
  selector: 'app-associado-detalhe',
  templateUrl: './associado-detalhe.component.html',
  styleUrls: ['./associado-detalhe.component.css'],
  providers: [DatePipe],
})
export class AssociadoDetalheComponent implements OnInit {
  modalRef: BsModalRef;
  associadoId: number;
  associado = {} as Associado;
  form: FormGroup;
  estadoSalvar = 'post';
  imagemURL = 'assets/img/upload.png';
  file: File;

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private associadoService: AssociadoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router,
    private datePipe: DatePipe
  ) {
    this.localeService.use('pt-br');
  }

  public carregarAssociado(): void {
    this.associadoId = +this.activatedRouter.snapshot.paramMap.get('id');

    if (this.associadoId !== null && this.associadoId !== 0) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.associadoService
        .getAssociadoById(this.associadoId)
        .subscribe((associado: Associado) => {
          this.associado = { ...associado };
          this.form.patchValue(this.associado);
          if (this.associado.imagemUrl !== '') {
            this.imagemURL = environment.apiURL + 'resources/images/' + this.associado.imagemUrl;
          }
        },
          (error: any) => {
            this.toastr.error('Erro ao tentar carregar Associado.', 'Erro!');
            console.error(error);
          }
        ).add(() => this.spinner.hide());
    }
  }

  ngOnInit(): void {
    this.carregarAssociado();
    this.validation();
  }

  protected initializeForm(model: Associado) {
    this.form = this.fb.group({
      id: [model.id, Validators.required],
      nome: [model.nome, Validators.maxLength(100)],
      imagemUrl: [model.imagemUrl],
      cpf: [model.cpf],
      sexo: [model.sexo],
      dataNascimento: [model.dataNascimento],
      celular: [model.celular],
      email: [model.email],
      ruaAvenida: [model.ruaAvenida, Validators.required],      
      numero: [model.numero, Validators.required],
      complemento: [model.complemento, Validators.required],
      bairro: [model.bairro, [Validators.required, Validators.email]],
      estadoNome: [model.estadoNome, [Validators.required]],
      cidadeNome: [model.cidadeNome, [Validators.required]],
    });
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      local: ['', Validators.required],
      dataAssociado: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: [''],
      lotes: this.fb.array([]),
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarAssociado(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.associado =
        this.estadoSalvar === 'post'
          ? { ...this.form.value }
          : { id: this.associado.id, ...this.form.value };

      this.associadoService[this.estadoSalvar](this.associado).subscribe(
        (associadoRetorno: Associado) => {
          this.toastr.success('Associado salvo com Sucesso!', 'Sucesso');
          this.router.navigate([`associados/detalhe/${associadoRetorno.id}`]);
        },
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toastr.error('Error ao salvar associado', 'Erro');
        },
        () => this.spinner.hide()
      );
    }
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {
    this.spinner.show();
    this.associadoService.postUpload(this.associadoId, this.file).subscribe(
      () => {
        this.carregarAssociado();
        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }
}
