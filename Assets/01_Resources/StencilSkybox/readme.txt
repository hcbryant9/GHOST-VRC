���T�v
�I�u�W�F�N�g�̒��ɓ������Ƃ���Skybox��؂�ւ���V�F�[�_�[�ł��B
V�P�b�g�ȂǂŌ��������u�u�[�X�̒��ɓ���ƕʋ�Ԃɕς��v�Ƃ������̂������Ȃ�ɍ���ăe���v���[�g���������̂ł��B
Stencil�𗘗p���邱�ƂŃT���v������̂悤��Skybox�����ł͂Ȃ��I�u�W�F�N�g�ɂ��K�p�\�ł��B

�{�V�F�[�_�[��Skybox��\��t���������Cube��\���E��\�����邱�ƂŎ�������Ă��܂��B
Cube�̃T�C�Y��傫������Ɖ��s�����͑����܂����A���̃I�u�W�F�N�g�Ɋ����Ȃ��悤�ɒ��ӂ��Ă��������B

�d�g�݂ɂ��Ă͉��L�T�C�g���Q��
����Tips - HoloLens �Ō��������������鑋�𓮓I�ɒǉ����Ă݂�
http://tips.hecomi.com/entry/2017/02/18/190949

���e�V�F�[�_�[�̐���
(1)StencilSkyB
�@�\��������Skybox��ݒ肷��V�F�[�_�[

�@�EMask
�@�@�@�e�V�F�[�_�[�ŋ��ʂ̒l���w��(�����l=1)
�@�EComp
�@�@�@Equal���w��
�@�@�@NotEqual���w�肷���Skybox���\�������悤�ɂȂ�̂�Skybox�̃T�C�Y��ύX����ۂɎg�p����
�@�ESkybox Size
�@�@�@Skybox�̃T�C�Y
�@�ECubemap
�@�@�@�\��������Skybox(Cubemap)���w��

(2)StencilWindow
�@���̃V�F�[�_�[��K�p�����I�u�W�F�N�g�����ɃJ�����������(1)�Ŏw�肵��Skybox���\�������

�@�EMask
�@�@�@�e�V�F�[�_�[�ŋ��ʂ̒l���w��(�����l=1)
�@�EEnabled Rendering Distance
�@�@�@Rendering Distance�̗L���E����
�@�@�@�����ɂ����ꍇ���̃V�F�[�_�[��K�p�����I�u�W�F�N�g����ɕ\�������悤�ɂȂ�A
�@�@�@���̃I�u�W�F�N�g��ʂ��ĕʋ�Ԃ�`�����Ƃ��ł��鑋�V�F�[�_�[�̂悤�ȕ\���ɂȂ�
  �ERendering Distance
�@�@�@���̃V�F�[�_�[��K�p�����I�u�W�F�N�g�ƃJ�����̋�����Rendering Distance�ȉ��̏ꍇ�ASkybox��؂�ւ���
�@�@�@�I�u�W�F�N�g�̒��S����̋����ƂȂ��Ă��邽�߁A�Ⴆ�΃T�C�Y��1x1x1��Cube��Rendering Distance=0.5���w�肷���
�@�@�@Cube�����ɃJ����������������Skybox���؂�ւ��悤�ɂȂ�

(3)StencilObject
�@Skybox�ȊO�ŕ\����؂�ւ������I�u�W�F�N�g�ɓK�p
�@��Stencil�������ڐA����Α��̃V�F�[�_�[�ɂ��K�p�\
�@
�@�EMask
�@�@�@�e�V�F�[�_�[�ŋ��ʂ̒l���w��(�����l=1)
�@�EComp
�@�@�@�ʏ펞�ɕ\���������I�u�W�F�N�g�ɂ�NotEqual�ASkybox�؂�ւ��Ƌ��ɕ\�����������̂ɂ�Equal���w��
�@�EMain Color
�@�@�@�I�u�W�F�N�g�̐F