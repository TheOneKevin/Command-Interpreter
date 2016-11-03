using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.ByteCode
{
    public enum OpCode : byte
    {
        reserved0 = 0x0,

        //Stack things
        push,
        ipush,
        swap,
        //Call thing
        call,

        reserved1,

        //Return
        ret,
        iret,

        //Handling exceptions & debugging
        magic,
        error,

        //Arithmatic stuff
        cmp = 0x0A,
        je,
        jne,
        jb,
        jbe,
        ja,
        jae,
        methodDef,
        classDef,
        namespaceDef,

        //Insert raw minecraft command
        _raw = 0x2A,
        
        irv, //Create a new variable
        ro,  //Create an new object

        //Interger pop and object pop
        ipop,
        pop,

        //Push variable value into stack
        spush,
        lpush,
        bpush,

        //Pop value from stack into variable
        spop,
        lpop,
        bpop,

        //Load/pops the top stack value into register (a, b, c or d), totalling 16 registers!
        lodr, //as, bs, cs, ds; al, bl, cl, dl; ab, bb, cb, db; ai, bi, ci, di

        //Move register value into register of same kind
        movr, //as, bs, cs, ds; al, bl, cl, dl; ab, bb, cb, db; ai, bi, ci, di

        //Pushes current register into stack
        pusr  //as, bs, cs, ds; al, bl, cl, dl; ab, bb, cb, db; ai, bi, ci, di
    };
}
