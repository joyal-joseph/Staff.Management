import { NgIf } from '@angular/common';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { isDelegatedFactoryMetadata } from '@angular/compiler/src/render3/r3_factory';
import { Component, OnInit, Output,EventEmitter } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { StaffService, Gender } from '../../../services/all-staffs.service';

export class BaseComponent{
    staffs:any[]=[];
    //const myObservable = of(this.staffs);
    allChecked=false;
    activePopUpForm=false;
    isAddForm=false;
    
}