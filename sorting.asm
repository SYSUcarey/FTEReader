########################### sorting ####################################
# This is a simple program written in assembly language
# User input 10 unsigned number, program will show them on the screen in order from 
#large to small
########################### 2018-04-09  #################################

#------------CODE SEGMENT-------------------#
.text
.globl main
main:
   li $v0, 4                # output string msg1
   la $a0, msg1             # msg1:"please give 10 unsigned number:\n"
   syscall
##--------get 10 numbers--------##
   la $t0, array            # send base address of array to $t0  
   addi $s0, $zero, 0       # let ($s0)=0 to count
   addi $s1, $zero, 10      # let ($s1)=10 to judge
get:
   li $v0, 5                # input a number and save in $v0
   syscall

   sw $v0, ($t0)            # save the number to $t0 (is array[i]) i FROM 0 TO 9
   addi $t0, 4              # i++
   addi $s0, 1 
   bne $s0, $s1, get        # if(i != 10) jump to get

##--------sort 10 numbers--------##
   la $t0, array
   addi $s0, $zero, 0
   addi $s1, $zero, 10
again:
   addi $s0, 1
   beq $s0, $s1, loop
   lw $t1, 0($t0)
   lw $t2, 4($t0)
   addi $t0, 4
   slt $s2, $t1, $t2        # IF t1<t2,s2=1; ELSE s2=0
   beq $s2, $zero, again
   move $t3, $t1
   move $t1, $t2
   move $t2, $t3
   sw $t1, -4($t0)
   sw $t2, 0($t0)
   j again
loop:
    addi $s0, $zero, 0
    addi $s1, -1
    addi $s2, $zero, 1
    beq $s1, $s2, show
    la $t0, array
    j again
    
###--------show 10 numbers--------###
show:
   li $v0, 4                # output string msg2
   la $a0, msg2             # msg1:"After sorting:\n" 
   syscall

   la $t0, array            # send base address of array to $t0 
   addi $s0, $zero, 0       # let ($s0)=0 to count
   addi $s1, $zero, 10      # let ($s1)=10 to judge
show_loop:
   li $v0, 1
   lw $a0, 0($t0)
   syscall

   li $v0, 4
   la $a0, spacestr
   syscall

   addi $t0, 4
   addi $s0, 1
   bne $s0, $s1, show_loop
   li $v0, 10
   syscall

#------------DATA SEGMENT-------------------#
.data
   array:
       .space 40   #40 bytes space for 10 number
   msg1:
       .asciiz "please give 10 unsigned number:\n"
   msg2:
       .asciiz "After sorting:\n"   
   spacestr:
       .asciiz " "
###----end of file